import React from "react";

import "./style.css";
import Quizzes from "../../components/Quizzes";

function HomePage(props) {
  return (
    <div className="homepage">
      <div className="homepage__searchbar--container">
        <input
          className="homepage__searchbar col-10 col-md-6 col-lg-5"
          type="text"
          placeholder="Looking for quizzes"
        />
        <i className="fas fa-search homepage__searchbar--icon"></i>
      </div>
      <Quizzes />
    </div>
  );
}

export default HomePage;
