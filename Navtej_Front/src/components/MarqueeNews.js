import React, { Fragment, useEffect } from "react";
import configData from "./Config";

export default function MarqueeNews(props) {
  const news = props.news;
  return (
    <Fragment>
        <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${news.Slug}`}>{news.TitleData.length > 0 ? news.TitleData[0].Translation : ''}
        </a> <i className="fa fa-angle-double-right"></i>
    </Fragment>
  );
}
