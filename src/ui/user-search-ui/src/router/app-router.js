import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import Header from '../components/header';
import AddUser from '../components/add-user';
import UsersList from '../components/users-list';

const AppRouter = () => {
  return (
    <BrowserRouter>
      <div>
        <Header />
        <div className="main-content">
          <Switch>
            <Route component={UsersList} path="/" exact={true} />
            <Route component={AddUser} path="/add-user" />
          </Switch>
        </div>
      </div>
    </BrowserRouter>
  );
};

export default AppRouter;