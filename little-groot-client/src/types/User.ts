export interface User {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
}

export interface AuthenticatationRequest {
  email: string;
  password: string;
  rememberMe: boolean;
}

export type AuthenticatationResponse = User & {
  token: string;
};

export interface RegistrationRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}
