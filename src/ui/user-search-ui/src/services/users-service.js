import http from "../http-common";

const BaseUrl = "https://localhost:5001/api/v1";

class UsersDataService {
  get(id) {
    return http.get(`${BaseUrl}/users/${id}`);
  }

  create(data) {
    return http.post(`${BaseUrl}/users/register`, data);
  }
}

export default new UsersDataService();
