import {
  AuthenticatationRequest,
  AuthenticatationResponse,
  User
} from '@/types/User';
import {
  clearAuthorizationToken,
  setAuthorizationToken
} from '@/utils/api-utils';
import Http from './Http';
import { LittleGrootApiUri } from './Uri';

export default class UserApi {
  public static async authenticate(requestPayload: AuthenticatationRequest) {
    const res = await Http.post<AuthenticatationResponse>(
      LittleGrootApiUri.User.AUTHENTICATION_URI,
      requestPayload
    );
    const userWithToken = res.data;
    if (res.status === 200) {
      setAuthorizationToken(userWithToken.token);
    }
    return userWithToken;
  }

  public static async getCurrentUser() {
    try {
      const res = await Http.get<User>(
        LittleGrootApiUri.User.GET_CURRENT_USER_URI
      );
      return res.data;
    } catch (e) {
      const res: Response = e.response;
      if (res.status === 401) {
        clearAuthorizationToken();
      }
    }
  }
}
