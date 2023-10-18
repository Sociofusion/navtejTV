import React, { Fragment, useEffect } from "react";
import Axios from "axios";
import OwlCarousel from 'react-owl-carousel'
import configData from '../Config';
import Skeleton from 'react-loading-skeleton'
import 'react-loading-skeleton/dist/skeleton.css'


export default function StateHeroArea(props){
    const homeSliderData = props.homeSlider;
    const rightSlider = props.rightSlider? props.rightSlider:[]

    const [settingData, setSettingData] = React.useState({
        YoutubeVideoURL: "",
      })
    
    
      const axiosConfig = {
        headers: {
          sessionToken: configData.SESSION_TOKEN,
        },
      };
    
      useEffect(() => {
        Axios.get(configData.SETTING_URL, axiosConfig).then((response) => {
          setSettingData(JSON.parse(response.data.payload)[0]);
        });
      }, []);


    const classes = props.classes;
    return(
        <Fragment>
            <section className="hero-area">
        <div className="container">
            <div className="row">

                <div className="col-lg-8">
                    <OwlCarousel
                    className="owl-theme"
                    items="1"
                    autoplay
                    loop
                    >
                        {homeSliderData.map((slider)=>{
                            return(
                                <div className="item intro-carousel">
                            <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${slider.Slug}`} className="single-news big">
                            <div className="content-wrapper">
                            <div className="tag" style={slider  ? { backgroundColor: slider.CategoryColor } : { backgroundColor: '#9c27b0' }}>
                                    {props.stateslug}
                                </div>
                                <img src={slider && slider.PostFiles && slider.PostFiles.length > 0 ? slider.PostFiles[0].AssetLiveUrl : "/assets/images/no-image.png" } alt= {slider.TitleData[0].Translation} />
                                <div className="inner-content">
                                    <span>
                                        <h4 className="title">
                                            {slider.TitleData[0].Translation}
                                        </h4>
                                    </span>

                                </div>
                                </div>
                            </a>
                        </div>
                            )
                        })}

                    </OwlCarousel>
                </div>
                <div className="col-lg-4 mycol">
              <div className="content-wrapper">
                <div className="side-video-hero">
                  <iframe class="heroYoutubeVideo" frameBorder="0" scrolling="no" marginHeight="0" marginWidth="0"
                    type="text/html" src={settingData.YoutubeVideoURL}></iframe>
                </div>
              </div>
              {rightSlider.map((slider, i) => {
                if (i < 1)
                  return (
                    <a
                      href={`${configData.BASE_URL_CATEGORY_DETAIL}${slider.Slug}`}
                      className={
                        "single-news animation " + (i == 0 ? "mt-15" : "")
                      }
                    >
                      <div className="content-wrapper">
                        <div className="tag" style={{backgroundColor: slider.CategoryColor}}>{slider.CategoryName}</div>
                        <img
                          src={slider && slider.PostFiles && slider.PostFiles.length > 0 ? slider.PostFiles[0].AssetLiveUrl : "/assets/images/no-image.png"}
                          alt=""
                          className="lazy"
                        />
                        <div className="inner-content">
                          <span>
                            <h4 className="title">
                              {slider.TitleData[0].Translation}
                            </h4>
                          </span>
                        </div>
                      </div>
                    </a>
                  );
              })}
            </div>

            </div>
        </div>
    </section>
        </Fragment>
    )

}