import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { RoleGuard } from './auth/role.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NotesComponent } from './components/notes/notes.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  } ,
   {path: 'home',
  component: HomeComponent,
  canActivate: [AuthGuard], 
 children: [
      {
          path: 'notes',
          component: NotesComponent,
          canActivate: [AuthGuard]
      },
      { path: 'ntoes/:UserId', component: NotesComponent ,  canActivate: [AuthGuard]},
      {
          path: 'users',
          component: UsersComponent,
          canActivate: [AuthGuard, RoleGuard]
      }/* ,
      { path: 'bank-account/:UserId', component: BankAccountComponent,   canActivate: [AuthGuard]},
      {
          path: 'charts',
          component: ChartsComponent,
          canActivate: [AuthGuard]
      },
      { path: 'charts/:UserId', component: ChartsComponent,   canActivate: [AuthGuard]},
      {
          path: 'categories',
          component: CategoriesComponent,
          canActivate: [AuthGuard]
      },
      { path: 'categories/:UserId', component: CategoriesComponent,   canActivate: [AuthGuard]},
      {
        path: 'settings',
        component: SettingComponent,
        canActivate: [AuthGuard, RoleGuard]
      },
      { path: 'settings/:UserId', component: SettingComponent,   canActivate: [AuthGuard, RoleGuard]},
      {
        path: 'memorandum',
        component: MemorandumComponent,
        canActivate: [AuthGuard]
      },
       { path: 'memorandum/:UserId', component: MemorandumComponent,   canActivate: [AuthGuard]}, */
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
