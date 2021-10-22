import React from "react";
import UserForm from "./user-form";
import UsersDataService from "../services/users-service";

const AddUser = ({ history }) => {
  const handleOnSubmit = (user) => {
    console.log(user);

    UsersDataService.create(user)
      .then((response) => {
        alert("User Created");
        history.push("/");
      })
      .catch((err) => alert("Ups, something went wrong"));
  };

  return (
    <React.Fragment>
      <UserForm handleOnSubmit={handleOnSubmit} />
    </React.Fragment>
  );
};

export default AddUser;
