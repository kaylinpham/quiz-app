import React from "react";
import "./style.css";

function QuizDetails(props) {
  const toggleQA = () => {};
  return (
    <div className="quiz__details--container">
      <div className="question__slides col-12 col-lg-6" onClick={toggleQA}>
        haha
      </div>
      <div className="quiz__details--navigation">
        <i className="fas fa-arrow-left"></i>&nbsp;&nbsp;&nbsp;
        <span className="quiz__details--index">1/109</span>
        &nbsp;&nbsp;&nbsp;
        <i className="fas fa-arrow-right"></i>
      </div>
    </div>
  );
}

export default QuizDetails;
