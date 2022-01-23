import React from "react";
import ReactPaginate from "react-paginate";

import "./style.css";
import Quiz from "../../components/Quiz";

function HomePage(props) {
  return (
    <div className="homepage">
      <div className="homepage__searchbar--container">
        <input
          className="homepage__searchbar col-10 col-md-6 col-lg-5"
          type="text"
          placeholder="Looking for quizzes"
        />
        <i className="fas fa-search homepage__searchbar--icon"></i>
      </div>
      <div className="homepage__quizzes row">
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
        <Quiz />
      </div>
      {/* <ReactPaginate
        breakLabel="..."
        nextLabel=">"
        onPageChange={handlePageClick}
        pageRangeDisplayed={5}
        pageCount={pageCount}
        previousLabel="<"
        renderOnZeroPageCount={null}
      /> */}
    </div>
  );
}

export default HomePage;
