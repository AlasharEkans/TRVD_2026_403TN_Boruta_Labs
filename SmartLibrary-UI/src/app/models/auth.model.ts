export interface LoginResponse {
    token: string;
  }
  
  export interface UserPayload {
    role: string;
    email: string;
    userId: string;
  }