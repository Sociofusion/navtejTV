import React, { Fragment, useEffect, useState } from "react";
import configData from "./Config";


export default function MainMenuSubCategoryTabColumn(props) {
  return (
      <div className="col-lg-3 col-md-4 col-sm-6 pr-0">
        <div className="single-news-menu">
          <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${props.slug}`}>
            <div className="content-wrapper">
              <div className="img">
                <div className="tag" style={{backgroundColor: props.CategoryColor}}>{props.categoryName}</div>
                <img
                  src={props.imageUrl}
                  alt=""
                />
              </div>
              <div className="inner-content">
                <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${props.slug}`}>
                  <h4 className="title">
                    {props.title}
                  </h4>
                  <ul className="post-meta">
                    <li>
                      <span href="#">{props.createdOn}</span>
                    </li>
                    <li>
                      <span>|</span>
                    </li>
                    <li>
                      <span href="#">{props.author}</span>
                    </li>
                  </ul>
                </a>
              </div>
            </div>
          </a>
        </div>
      </div>
  )
}
