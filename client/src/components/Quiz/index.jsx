import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import avatar from "../../assets/images/avatar.jpeg";
import "./style.css";

function Quiz(props) {
  const features = [
    { name: "Copy link", className: "feature__item border-radius-top-5" },
    { name: "Edit", className: "feature__item" },
    { name: "Delete", className: "feature__item border-radius-bottom-5" },
  ];

  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();

  const toggleFeatures = () => {
    setIsOpen(!isOpen);
  };

  const openQuizDetails = (quiz) => {
    navigate("/quiz/1");
  };

  useEffect(() => {
    const closePopup = () => {
      if (isOpen) setIsOpen(false);
    };
    window.addEventListener("click", closePopup);
    return () => {
      window.removeEventListener("click", closePopup);
    };
  });

  return (
    <div
      className="quiz__container col-12 col-sm-6 col-md-4 col-lg-3"
      onClick={openQuizDetails}
    >
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
            <i className="fas fa-ellipsis-v" onClick={toggleFeatures}></i>
          </div>
        </div>
      </div>
      {isOpen && (
        <div className="feature__popup">
          <ul className="feature__list">
            {features.map((item, index) => (
              <li key={index} className={item.className}>
                {item.name}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}

export default Quiz;
