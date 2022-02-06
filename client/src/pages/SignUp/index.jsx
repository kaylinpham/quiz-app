import { React, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./style.css";

function SignUp(props) {
  const [user, setUser] = useState({
    username: "",
    password: "",
    confirm: "",
  });
  const [isValid, setIsValid] = useState(true);

  let navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    // axios
    //   .post(`${REQUEST_URL}/user/login`, person, {
    //     headers: {
    //       "Content-Type": "application/json",
    //     },
    //   })
    //   .then((res) => {
    //     if (res.status !== 200) {
    //       console.log(res);
    //       setIsValid(false);
    //       setPerson({
    //         password: "",
    //         email: "",
    //       });
    //     } else {
    //       localStorage.setItem("user", JSON.stringify(res.data.data));
    //       navigate("/home");
    //     }
    //   })
    //   .catch((err) => {
    //     setIsValid(false);
    //     console.log(err);
    //   });
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
            id="password"
            name="password"
            value={user.confirm}
            onChange={handleChange}
          />
        </div>
        {!isValid && (
          <p className="message__invalid">Thông tin không phù hợp</p>
        )}
        <button className="login__btn" onClick={handleSubmit}>
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
