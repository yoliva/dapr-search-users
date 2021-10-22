import React, { useState, useEffect } from "react";
import { ListGroup, Spinner } from "react-bootstrap";
import InfiniteScroll from "react-infinite-scroll-component";
import SearchUsersService from "../services/search-service";
import SearchInput from "./search-input";

const MAX_USER_COUNT = 10;
const UsersList = ({ history }) => {
  const [users, setUsers] = useState([]);
  const [isInitialized, setIsInitialized] = useState(false);
  const [nextPageToken, setNextPageToken] = useState("");
  const [query, setQuery] = useState("");
  const [hasMoreResults, setHasMoreResults] = useState(true);

  const fetchNextData = (continuationToken = "") => {
    if (!isInitialized) setIsInitialized(true);
    SearchUsersService.searchUsers(
      query,
      MAX_USER_COUNT,
      continuationToken
    ).then((response) => {
      const data = response.data.data.map((usr, index) => ({
        username: usr.username,
        fullName: usr.fullName,
      }));

      if (data.length === 0) {
        setHasMoreResults(false);
        return;
      }

      setUsers(continuationToken === "" ? data : users.concat(data));
      setNextPageToken(response.data.continuationToken);
    });
  };

  useEffect(() => {
    if (!isInitialized) {
      fetchNextData();
    }
  });

  const navigateToProfile = (username) => {
    history.push({
      pathname: "/user-profile",
      search: `?username=${username}`,
    });
  };

  const handleInputChange = (event) => {
    setQuery(event.target.value);

    if (event.target.value.endsWith(" ")) {
      fetchNextData("");
    }
  };
  if (users.length === 0) {
    return (
      <div className="main-form">
        <SearchInput
          query={query}
          handleInputChange={handleInputChange}
        ></SearchInput>
        <p style={{ textAlign: "center" }}>
          <b>No data available :(</b>
        </p>
      </div>
    );
  } else {
    return (
      <div className="main-form">
        <SearchInput
          query={query}
          handleInputChange={handleInputChange}
        ></SearchInput>

        <ListGroup>
          <InfiniteScroll
            dataLength={users.length}
            next={() => fetchNextData(nextPageToken)}
            hasMore={hasMoreResults}
            loader={<Spinner animation="border" />}
            endMessage={
              <p style={{ textAlign: "center" }}>
                <b>Yay! You have seen it all</b>
              </p>
            }
          >
            {users.map((user, index) => (
              <ListGroup.Item
                key={user.username}
                action
                onClick={() => navigateToProfile(user.username)}
              >
                {user.fullName} - {user.username}
              </ListGroup.Item>
            ))}
          </InfiniteScroll>
        </ListGroup>
      </div>
    );
  }
};

export default UsersList;
