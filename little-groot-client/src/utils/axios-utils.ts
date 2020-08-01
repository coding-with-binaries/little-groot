export function getAuthorizationHeader() {
  const authToken = localStorage.getItem('groot-auth-token');
  if (authToken) {
    return `Bearer ${authToken}`;
  }
  return null;
}

export function clearAuthorizationToken() {
  localStorage.removeItem('groot-auth-token');
}
