import { React, useState } from "react";
import { Link } from "react-router-dom";

import Question from "../../components/Question";
import "./style.css";

function Editting(props) {
  const [quiz, setQuiz] = useState({
    title: "",
    setOfQuestions: [],
  });

  const handleChange = (e) => {
    const name = e.target.name;
    const value = e.target.value;
    setQuiz((quiz) => {
      return { ...quiz, [name]: value };
    });
  };

  return (
    <div className="editting__page">
      <form className="editting__form col-6">
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
          <label htmlFor="setOfQuestions">Questions</label>
          <div className="questions">
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
            <Question />
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
