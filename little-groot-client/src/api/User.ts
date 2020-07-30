import {
  AuthenticatationRequest,
  AuthenticatationResponse,
  User
} from '@/types/User';
import axios from 'axios';
import { LittleGrootApiUri } from './Uri';
import { getAuthorizationHeader } from '@/utils/axios-utils';

axios.defaults.headers.common['Authorization'] = getAuthorizationHeader();

export default class UserApi {
  public static async authenticate(requestPayload: AuthenticatationRequest) {
    const res = await axios.post<AuthenticatationResponse>(
      LittleGrootApiUri.User.AUTHENTICATION_URI,
      requestPayload
    );
    return res.data;
  }

  public static async getCurrentUser() {
    const res = await axios.get<User>(
      LittleGrootApiUri.User.GET_CURRENT_USER_URI
    );
    console.log(res);
    return res.data;
  }
}
