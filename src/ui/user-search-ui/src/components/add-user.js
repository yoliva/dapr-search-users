import React from 'react';
import UserForm from './user-form';

const AddUser = () => {
  const handleOnSubmit = (user) => {
    console.log(user);
  };

  return (
    <React.Fragment>
      <UserForm handleOnSubmit={handleOnSubmit} />
    </React.Fragment>
  );
};

export default AddUser;