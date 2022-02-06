import React from "react";
import "./style.css";

function Question(props) {
  return (
    <div className="question__container">
      <span className="question__order">1</span>
      <textarea
        placeholder="Enter the question"
        name=""
        id=""
        className="col-8"
        rows="3"
      ></textarea>
      <textarea
        placeholder="Enter the answer"
        name=""
        id=""
        className="col-3"
        rows="3"
      ></textarea>
    </div>
  );
}

export default Question;
