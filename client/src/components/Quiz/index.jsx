import React from "react";

import "./style.css";
import avatar from "../../assets/images/avatar.jpeg";

function Quiz(props) {
  return (
    <div className="quiz__container col-12 col-sm-6 col-md-4 col-lg-3">
      <div className="quiz">
        <p className="quiz__title text-truncate">
          <b>Vocabulary</b>
        </p>
        <br />
        <p className="quiz__quantity">109 terms</p>
        <br />
        <div className="quiz__creator">
          <img
            className="quiz__creator--avt"
            src={avatar}
            alt="Go to profile"
          />
          <span className="quiz__creator--name">&nbsp; kaylinpham</span>
          <div className="quiz__more">
            <i className="fas fa-ellipsis-v"></i>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Quiz;
