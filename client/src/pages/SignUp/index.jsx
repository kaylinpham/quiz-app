import { React, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import * as _ from "lodash";
import axios from "axios";

import { REQUEST_URL } from "../../constants/api-constants";
import "./style.css";

function SignUp(props) {
  const [user, setUser] = useState({
    username: "",
    password: "",
    confirm: "",
  });
  const [isValid, setIsValid] = useState(false);

  let navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .post(`${REQUEST_URL}/user/register`, _.pick(user, ['username', 'password']), {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then((res) => {
        if (res.status !== 200) {
          console.log(res);
          setIsValid(false);
          setUser({
            password: "",
            email: "",
          });
        } else {
          localStorage.setItem("user", JSON.stringify(res.data.data));
          navigate("/auth/login");
        }
      })
      .catch((err) => {
        setIsValid(false);
        console.log(err);
      });
  };

  const handleChange = (e) => {
    const name = e.target.name;
    const value = e.target.value;
    setUser((person) => {
      return { ...person, [name]: value };
    });
  };

  useEffect(() => {
    if (
      user.username.trim().length >= 6 &&
      user.password.length >= 6 &&
      user.password === user.confirm
    )
      setIsValid(true);
    else setIsValid(false);
  }, [user.username, user.password, user.confirm]);

  return (
    <div className="login__form--container">
      <form className="login__form col-md-6 col-lg-5">
        <div className="form-control">
          <label htmlFor="username">Username*</label>
          <input
            className="form__input"
            type="text"
            id="username"
            name="username"
            value={user.username}
            onChange={handleChange}
          />
        </div>
        <div className="form-control">
          <label htmlFor="password">Password*</label>
          <input
            className="form__input"
            type="password"
            id="password"
            name="password"
            value={user.password}
            onChange={handleChange}
          />
        </div>
        <div className="form-control">
          <label htmlFor="password">Confirm Password*</label>
          <input
            className="form__input"
            type="password"
            id="confirm"
            name="confirm"
            value={user.confirm}
            onChange={handleChange}
          />
        </div>
        <button
          className="login__btn"
          onClick={handleSubmit}
          disabled={!isValid}
        >
          Sign up
        </button>
        <p className="message__navigate">
          Already have account? <Link to="/auth/login">Login</Link>
        </p>
      </form>
    </div>
  );
}

export default SignUp;
