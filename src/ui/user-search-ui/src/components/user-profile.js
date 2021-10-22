import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import UsersDataService from "../services/users-service";

function UserProfile() {
  const username = new URLSearchParams(useLocation().search).get("username");
  const [isInitialized, setIsInitialized] = useState(false);
  const [user, setUser] = useState(null);

  useEffect(() => {
    if (!isInitialized) {
      UsersDataService.get(username).then((response) => {
        setIsInitialized(true);
        setUser(response.data);
      });
    }
  });

  return (
    <h1>
      Hello {user?.fullName}! Your username is:{user?.username}
    </h1>
  );
}

export default UserProfile;
