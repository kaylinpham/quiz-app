import React from "react";
import { Link } from "react-router-dom";

import avatar from "../../assets/images/avatar.jpeg";
import Quizzes from "../../components/Quizzes";
import "./style.css";

function Profile(props) {
  return (
    <div className="profile">
      <div className="profile__avatar--container">
        <img className="profile__avatar--image" src={avatar} alt="Avatar" />
      </div>
      <p className="profile__username">kaylinpham</p>
      <div className="my__quizzes--title">
        <h2>Yours &nbsp;</h2>
        <Link to="/me/quiz">
          <span className="add__quiz--btn">
            <i title="Add new quiz" className="fas fa-plus-circle"></i>
          </span>
        </Link>
      </div>
      <Quizzes />
    </div>
  );
}

export default Profile;
