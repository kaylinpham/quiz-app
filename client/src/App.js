import {
  BrowserRouter as Router,
  Routes,
  Route,
  NavLink,
} from "react-router-dom";

import "./App.css";
import Favorite from "./pages/Favorite";
import History from "./pages/History";
import HomePage from "./pages/HomePage";
import Login from "./pages/Login";
import NotFound from "./pages/NotFound";
import Profile from "./pages/Profile";
import SignUp from "./pages/SignUp";

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
  {
    title: "Favorite",
    class: "fas fa-heart",
    path: "/user/favorite",
  },
  {
    title: "Recently",
    class: "fas fa-history",
    path: "/history",
  },
];

function App() {
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
            <li
              title="Signout"
              key="Signout"
              className="app__navbar--item signout__item"
            >
              <i className="fas fa-sign-out-alt"></i>
            </li>
          </ul>
        </div>
        <div className="app_router">
          <Routes>
            <Route exact path="/" element={<HomePage />}></Route>
            <Route exact path="/me" element={<Profile />}></Route>
            <Route exact path="/auth/login" element={<Login />}></Route>
            <Route exact path="/auth/signup" element={<SignUp />}></Route>
            <Route exact path="/user/favorite" element={<Favorite />}></Route>
            <Route exact path="/history" element={<History />}></Route>
            <Route path="*" element={<NotFound />}></Route>
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
