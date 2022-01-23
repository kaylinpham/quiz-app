import { BrowserRouter as Router } from "react-router-dom";

import "./App.css";
import HomePage from "./pages/HomePage";

const NAVBAR_ITEMS = [
  {
    title: "Home",
    class: "fas fa-home",
  },
  {
    title: "Profile",
    class: "fas fa-user",
  },
  {
    title: "Favorite",
    class: "fas fa-heart",
  },
  {
    title: "Recently",
    class: "fas fa-history",
  },
  {
    title: "Signout",
    class: "fas fa-sign-out-alt",
  },
];

function App() {
  return (
    <Router>
      <div className="app__container">
        <div className="app__navbar">
          <ul className="app__navbar--list">
            {NAVBAR_ITEMS.map((item) => (
              <li
                title={item.title}
                key={item.title}
                className="app__navbar--item"
              >
                <i className={item.class}></i>
              </li>
            ))}
          </ul>
        </div>
        <div className="app_router">
          {/* <Routes> */}
          {/* <Route path="/"> */}
          <HomePage />
          {/* </Route> */}
          {/* </Routes> */}
        </div>
      </div>
    </Router>
  );
}

export default App;
