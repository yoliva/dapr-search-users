import React from "react";
import { Form } from "react-bootstrap";

const SearchInput = (props) => {
  return (
    <Form>
      <Form.Group className="mb-3" controlId="query">
        <Form.Control
          type="text"
          placeholder="Search users..."
          name="query"
          value={props.query}
          onChange={props.handleInputChange}
        />
        <Form.Text className="text-muted"></Form.Text>
      </Form.Group>
    </Form>
  );
};

export default SearchInput;
