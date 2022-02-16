import { React, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import "./style.css";
import { REQUEST_URL } from "../../constants/api-constants";

function Login({ onReRender }) {
  const [user, setUser] = useState({
    username: "",
    password: "",
  });
  const [isValid, setIsValid] = useState(true);

  let navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .post(`${REQUEST_URL}/user/login`, user, {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then((res) => {
        if (res.status !== 200) {
          setIsValid(false);
          setUser({
            username: "",
            password: "",
          });
          // addToast(res.Message, { appearance: "error" });
        } else {
          localStorage.setItem("Authorization", res.data);
          navigate("/me");
          onReRender();
        }
      })
      .catch((err) => {
        setIsValid(false);
        // addToast(err, { appearance: "error" });
      });
  };

  const handleChange = (e) => {
    const name = e.target.name;
    const value = e.target.value;
    setUser((person) => {
      return { ...person, [name]: value };
    });
  };

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
            required
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
            required
          />
        </div>
        {!isValid && (
          <p className="message__invalid">Thông tin không phù hợp</p>
        )}
        <button className="login__btn" onClick={handleSubmit}>
          Login
        </button>
        <p className="message__navigate">
          Create new account? <Link to="/auth/signup">Sign up</Link>
        </p>
      </form>
    </div>
  );
}

export default Login;
