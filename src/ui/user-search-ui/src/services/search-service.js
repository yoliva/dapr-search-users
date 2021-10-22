import http from "../http-common";

class SearchUsersService {
  searchUsers(query, limit, continuationToken) {
    return http.get(
      `https://localhost:10000/api/v1/usersearch?query=${query}&limit=${limit}&continuationToken=${continuationToken}`
    );
  }
}

export default new SearchUsersService();
