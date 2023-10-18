import React, { Fragment, useEffect } from "react";
import MarqueeNews from "./MarqueeNews";

export default function TopHeaderScroll(props) {
  const marqueeNews = props.marqueeNews;
  const site_lang = props.site_lang;
  useEffect(() => {
    const marqueeNews = document.querySelector("#marqueeNews");
    marqueeNews.addEventListener("mouseover", function () {
      marqueeNews.stop();
    });
    marqueeNews.addEventListener("mouseout", function () {
      marqueeNews.start();
    });
  }, []);

  return (
    <Fragment>
      <section className="top-header">
        <div className="container">
          <div className="row">
            <div className="col-lg-12">
              <div className="content">
                <div className="left-content">
                  <div className="heading">
                    <span>{site_lang == 3 ? "अभी का दौर" : "Breaking News"} </span>
                  </div>
                  <span className="arrowRight"></span>
                  <div className="marquee">
                    <marquee id="marqueeNews" scrollamount="5">
                      {marqueeNews && marqueeNews.length > 0 ? marqueeNews.map((news) => {
                        return <MarqueeNews news={news} />;
                      }):''}
                    </marquee>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
}
