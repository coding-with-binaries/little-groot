export namespace LittleGrootApiUri {
  const BASE_URI = '/api/v1';

  export namespace User {
    export const USERS_URI = `${BASE_URI}/users`;

    export const AUTHENTICATION_URI = `${USERS_URI}/authenticate`;

    export const GET_CURRENT_USER_URI = `${USERS_URI}/current-user`;
  }
}
