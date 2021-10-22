import "bootstrap/dist/css/bootstrap.min.css";
import { Switch, Route, Link } from "react-router-dom";
import AddUser from "./components/add-user";
import UserProfile from "./components/user-profile";
import UsersList from "./components/users-list";
import './App.css';

function App() {
  return (
      <div>
        <nav className="navbar navbar-expand navbar-dark bg-dark">
          <a href="/users-list" className="navbar-brand">
            bezKoder
          </a>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/users-list"} className="nav-link">
                Users List
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/add-user"} className="nav-link">
                Add User
              </Link>
            </li>
          </div>
        </nav>

        <div className="container mt-3">
          <Switch>
            <Route exact path={["/", "/users-list"]} component={UsersList} />
            <Route exact path="/add-user" component={AddUser} />
            <Route path="/user/:id" component={UserProfile} />
          </Switch>
        </div>
      </div>
    );
}

export default App;
