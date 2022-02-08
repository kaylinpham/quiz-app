import {
  BrowserRouter as Router,
  Routes,
  Route,
  NavLink,
} from "react-router-dom";

import "./App.css";
import Editting from "./pages/Editting";
import HomePage from "./pages/HomePage";
import Login from "./pages/Login";
import NotFound from "./pages/NotFound";
import Profile from "./pages/Profile";
import QuizDetails from "./pages/QuizDetails";
import SignUp from "./pages/SignUp";
import ProtectedRoute from "./routes/ProtectedRoute";

const NAVBAR_ITEMS = [
  {
    title: "Home",
    class: "fas fa-home",
    path: "/",
  },
  {
    title: "Profile",
    class: "fas fa-user",
    path: "/me",
  },
  // {
  //   title: "Favorite",
  //   class: "fas fa-heart",
  //   path: "/user/favorite",
  // },
];

function App() {
  const token = localStorage.getItem("Authorization");

  return (
    <Router>
      <div className="app__container">
        <div className="app__navbar">
          <ul className="app__navbar--list">
            {NAVBAR_ITEMS.map((item) => (
              <NavLink
                title={item.title}
                key={item.title}
                to={item.path}
                className={({ isActive }) =>
                  "app__navbar--item" + (isActive ? " app__navbar--active" : "")
                }
              >
                <i className={item.class}></i>
              </NavLink>
            ))}
            {!!token && (
              <li
                title="Signout"
                key="Signout"
                className="app__navbar--item signout__item"
              >
                <i className="fas fa-sign-out-alt"></i>
              </li>
            )}
          </ul>
        </div>
        <div className="app_router">
          <Routes>
            <Route exact path="/" element={<HomePage />}></Route>
            <Route exact path="/me" element={<ProtectedRoute />}>
              <Route exact path="/me" element={<Profile />} />
              <Route exact path="/me/quiz" element={<Editting />} />
            </Route>
            <Route exact path="/auth/login" element={<Login />}></Route>
            <Route exact path="/auth/signup" element={<SignUp />}></Route>
            <Route exact path="/quiz/:id" element={<QuizDetails />}></Route>
            <Route path="*" element={<NotFound />}></Route>
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
