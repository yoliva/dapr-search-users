import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";

const UserForm = (props) => {
  const [user, setUser] = useState({
    firstName: props.user ? props.user.firstName : "",
    lastName: props.user ? props.user.lastName : "",
    username: props.user ? props.user.username : "",
  });

  const [errorMsg, setErrorMsg] = useState("");
  const { firstName, lastName, username } = user;

  const handleOnSubmit = (event) => {
    event.preventDefault();
    const values = [firstName, lastName, username];
    let errorMsg = "";

    const allFieldsFilled = values.every((field) => {
      const value = `${field}`.trim();
      return value !== "" && value !== "0";
    });

    if (allFieldsFilled) {
      const user = {
        firstName,
        lastName,
        username,
      };
      props.handleOnSubmit(user);
    } else {
      errorMsg = "Please fill out all the fields.";
    }
    setErrorMsg(errorMsg);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setUser((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  return (
    <div className="main-form">
      {errorMsg && <p className="errorMsg">{errorMsg}</p>}
      <Form onSubmit={handleOnSubmit}>
        <Form.Group controlId="firstName">
          <Form.Label>First Name</Form.Label>
          <Form.Control
            className="input-control"
            type="text"
            name="firstName"
            value={firstName}
            placeholder="Enter first name"
            onChange={handleInputChange}
          />
        </Form.Group>
        <Form.Group controlId="lastName">
          <Form.Label>Last Name</Form.Label>
          <Form.Control
            className="input-control"
            type="text"
            name="lastName"
            value={lastName}
            placeholder="Enter last name"
            onChange={handleInputChange}
          />
        </Form.Group>
        <Form.Group controlId="username">
          <Form.Label>Username</Form.Label>
          <Form.Control
            className="input-control"
            type="text"
            name="username"
            value={username}
            placeholder="Enter available username"
            onChange={handleInputChange}
          />
        </Form.Group>
        <Button variant="primary" type="submit" className="submit-btn">
          Submit
        </Button>
      </Form>
    </div>
  );
};

export default UserForm;
