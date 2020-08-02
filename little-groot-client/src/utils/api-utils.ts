export function getAuthorizationHeader() {
  const authToken = localStorage.getItem('groot-auth-token');
  if (authToken) {
    return `Bearer ${authToken}`;
  }
  return null;
}

export function setAuthorizationToken(token: string) {
  localStorage.setItem('groot-auth-token', token);
}

export function clearAuthorizationToken() {
  localStorage.removeItem('groot-auth-token');
}
