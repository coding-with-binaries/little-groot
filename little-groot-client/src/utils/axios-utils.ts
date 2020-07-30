export function getAuthorizationHeader() {
  const authToken = localStorage.getItem('groot-auth-token');
  if (authToken) {
    return `Bearer ${authToken}`;
  }
  return null;
}
