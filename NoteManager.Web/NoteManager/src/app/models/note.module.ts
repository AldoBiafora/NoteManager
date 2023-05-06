export interface LoginDTO {
    Username: string;
    Password: string;
}

export interface CookieDTO {
    Token: string;
    CompleteName: string;
    UserId: number;
    Role: string;
}

export interface BaseUserDTO {
    Name: string,
    LastName: string;
    Login: LoginDTO;
}

export interface ConfirmDialogModel {
  title: string,
  message: string,
  mode: DialogMode,
  value: number;
}

export enum DialogMode {

  ALERT = 0,
  CONFIRM = 1

}
