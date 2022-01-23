import React from "react";
import Quizzes from "../../components/Quizzes";
import avatar from "../../assets/images/avatar.jpeg";
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
        <span className="add__quiz--btn">
          <i title="Add new quiz" class="fas fa-plus-circle"></i>
        </span>
      </div>
      <Quizzes />
    </div>
  );
}

export default Profile;
