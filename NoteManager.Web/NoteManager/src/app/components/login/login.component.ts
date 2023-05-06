import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { LoginDTO } from 'src/app/models/note.module';
import { NoteService } from 'src/app/services/note.service';
import { ToolService } from 'src/app/services/tool.service';
import { AppSettings } from 'src/app/shared/app.settings';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm = this.formBuilder.group({
    email: [new FormControl, Validators.required],
    password: [new FormControl, Validators.required]
  })

  isLoading: boolean = false;

  constructor(private formBuilder: FormBuilder,
              private _service: NoteService,
              private cookieService: CookieService,
              private toolService: ToolService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm.reset()
  }

  onSubmitLogin(): void {
    this.isLoading = true;
    let formData = this.loginForm.value;
    let returnData: LoginDTO = {
      Username: formData.email,
      Password: formData.password /* this.toolService.hashMd5(formData.password) */
    }

    this._service.login(returnData).subscribe(result => {
      console.log(result)
      if(result != null){
      let token: string = result.Token;
        this.cookieService.set(AppSettings.TOKEN_NAME, token, 0, '/')
        this.isLoading = false;
        sessionStorage.setItem('Role', result.Role);
        sessionStorage.setItem('UserId', result.UserId.toString()); 
        this.router.navigate(['./home/notes']);
      }else {
        this.toolService.openSnackBarError('Attenzione! Email o Password errate, Riprovare', '')
        this.isLoading = false;
        this.loginForm.reset();
      }
    });
  }
}
