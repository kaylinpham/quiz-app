import { React, useState, useRef } from "react";
import { Link } from "react-router-dom";

import Question from "../../components/Question";
import { PRIVATE, PUBLIC } from "../../constants/quiz-constants";
import "./style.css";

function Editting(props) {
  const [quiz, setQuiz] = useState({
    title: "",
    setOfQuestions: [],
    accessibility: PUBLIC,
  });
  const [questionSets, setQuestionSets] = useState([
    { question: "", answer: "" },
    { question: "", answer: "" },
    { question: "", answer: "" },
    { question: "", answer: "" },
    { question: "", answer: "" },
    { question: "", answer: "" },
    { question: "", answer: "" },
  ]);
  const scrollPointRef = useRef(null);

  const handleChange = (e) => {
    const name = e.target.name;
    const value = e.target.value;
    setQuiz((quiz) => {
      return { ...quiz, [name]: value };
    });
  };

  const addQuestionSet = () => {
    setQuestionSets([...questionSets, { question: "", answer: "" }]);
    scrollPointRef.current.scrollIntoView({ block: "start" });
  };

  return (
    <div className="editting__page">
      <form className="editting__form col-12 col-md-8 col-lg-6">
        <div className="form-control">
          <label htmlFor="title">Title*</label>
          <input
            className="form__input"
            type="text"
            id="title"
            name="title"
            value={quiz.title}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-control">
          <label htmlFor="title">Accessibility*</label>
          <div className="quiz__accessibility">
            <div className="quiz__accessibility--option">
              <input
                className="form__input--radio"
                type="radio"
                id="accessibility_public"
                name="accessibility"
                value={PUBLIC}
                onChange={handleChange}
                checked
              />
              <label htmlFor="accessibility_public">Public</label>
            </div>
            <div className="quiz__accessibility--option">
              <input
                className="form__input--radio"
                type="radio"
                id="accessibility_private"
                name="accessibility"
                value={PRIVATE}
                onChange={handleChange}
              />
              <label htmlFor="accessibility_private">Private</label>
            </div>
          </div>
        </div>
        <div className="form-control">
          <label htmlFor="setOfQuestions">Questions</label>
          <span className="add__set--btn" onClick={addQuestionSet}>
            <i title="Add new quiz" className="fas fa-plus-circle"></i>
          </span>
          <div className="questions">
            {questionSets.map((set, index) => {
              return <Question key={index} order={index + 1} />;
            })}
            <div className="scroll__point" ref={scrollPointRef}></div>
          </div>
        </div>
        <div className="editting__action">
          <button type="submit" className="editting__action--save">
            Save
          </button>
          <Link to="/me">
            <button className="editting__action--cancel">Cancel</button>
          </Link>
        </div>
      </form>
    </div>
  );
}

export default Editting;
