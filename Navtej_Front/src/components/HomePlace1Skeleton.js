import React, { Fragment, useEffect,useState } from "react";
import configData from "./Config";
import Axios from "axios";
import parse from "html-react-parser";
import Skeleton, {SkeletonTheme} from 'react-loading-skeleton'
import 'react-loading-skeleton/dist/skeleton.css'

export default function HomePlace1Skeleton(props) {





  return (
    <Fragment>
      <section className="home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-8">
              {/* <!-- News Tabs start --> */}
              <div className="main-content tab-view border-theme">
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="header-area">
                      <h3 className="title">
                        <Skeleton width={100} />
                      </h3>
                      <Skeleton width={50} />
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-lg-12">
                    <div className="tab-content" id="pills-tabContent">
                      <div
                        className="tab-pane fade show active"
                        id="pills-Rajasthan"
                        role="tabpanel"
                        aria-labelledby="pills-Rajasthan-tab"
                      >
                        <div className="row">
                          <div className="col-md-6 mycol">
                            <div className="single-news landScape-normal box_design_block">

                                <div className="content-wrapper">
                                  <div className="img">

                                    <Skeleton width={335} height={188} />
                                  </div>
                                  <div className="inner-content">
                                      <h4 className="title">
                                        <Skeleton />
                                      </h4>
                                      <p className="text">
                                        <Skeleton height={100}/>
                                      </p>
                                  </div>
                                </div>
                            </div>
                          </div>
                          <div className="col-md-6 mycol">
                            <div className="row">

                                  <div className="col-md-6 r-p">
                                    <a href="">
                                      <div className="single-box landScape-small-with-meta box_design_block">
                                        <div className="img">
                                          <Skeleton width={167} height={100}/>
                                        </div>
                                        <div className="content">
                                          <a href="">
                                            <h4 className="title">
                                            <Skeleton height={100}/>
                                            </h4>
                                          </a>
                                        </div>
                                      </div>
                                    </a>
                                  </div>

                                  <div className="col-md-6 r-p">
                                    <a href="">
                                      <div className="single-box landScape-small-with-meta box_design_block">
                                        <div className="img">
                                          <Skeleton width={167} height={100}/>
                                        </div>
                                        <div className="content">
                                          <a href="">
                                            <h4 className="title">
                                            <Skeleton height={100}/>
                                            </h4>
                                          </a>
                                        </div>
                                      </div>
                                    </a>
                                  </div>
                                  <div className="col-md-6 r-p">
                                    <a href="">
                                      <div className="single-box landScape-small-with-meta box_design_block">
                                        <div className="img">
                                          <Skeleton width={167} height={100}/>
                                        </div>
                                        <div className="content">
                                          <a href="">
                                            <h4 className="title">
                                            <Skeleton height={100}/>
                                            </h4>
                                          </a>
                                        </div>
                                      </div>
                                    </a>
                                  </div>
                                  <div className="col-md-6 r-p">
                                    <a href="">
                                      <div className="single-box landScape-small-with-meta box_design_block">
                                        <div className="img">
                                          <Skeleton width={167} height={100}/>
                                        </div>
                                        <div className="content">
                                          <a href="">
                                            <h4 className="title">
                                            <Skeleton height={100}/>
                                            </h4>
                                          </a>
                                        </div>
                                      </div>
                                    </a>
                                  </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div className="col-lg-4">
              {/* <!-- सोशल मीडिया start --> */}
              <div className="main-content tab-view border-theme">
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="header-area">
                      <Skeleton/>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="blank"></div>
                  </div>
                </div>
              </div>
              {/* <!-- सोशल मीडिया emd --> */}
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
}
