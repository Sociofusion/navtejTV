import React, { Fragment, useEffect, useState } from 'react'
import configData from './Config';
import RelatedNewsBlock from './RelatedNewsBlock'
import Axios from "axios";




export default function RelatedNews(props){
    const relatedNewsApiUrl = configData.RELATED_NEWS_API_URL + props.postId;
    const axiosConfig = {
        headers: {
          sessionToken: configData.SESSION_TOKEN,
        },
      };

    const [relatedNews, setRelatedNews] = React.useState([]);

     useEffect(()=>{
         if(props.postId){

         
    if(relatedNews && relatedNews.length < 1){
        Axios.get(relatedNewsApiUrl, axiosConfig).then((response) => {
            // console.log("data from server related",response.data.payload)
            // console.log("data from server used api related",relatedNewsApiUrl)
            setRelatedNews(JSON.parse(response.data.payload));
        }).catch(e=>{
            // console.log("data from server",e)
        })
    }
}
     },[props.postId]);
    if(!relatedNews) return null;
    return(
        <Fragment>
            <section className="home-front-area">
        <div className="container">
            <div className="row">
                <div className="col-lg-12">
                    {/* <!-- News Tabs start --> */}
                    <div className="main-content tab-view border-theme HomePlace7">
                        <div className="row">
                            <div className="col-lg-12 mycol">
                                <div className="header-area">
                                    <h3 className="title">
                                        Related News
                                    </h3>
                                    {/* <a href="#">सभी देखें</a> */}
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-lg-12">
                                <div className="tab-content" id="pills-tabContent">
                                    <div className="tab-pane fade show active" id="pills-Rajasthan" role="tabpanel" aria-labelledby="pills-Rajasthan-tab">
                                        <div className="row">
                                            {relatedNews.map((news,i) => {
                                                return(
                                                <RelatedNewsBlock
                                                    slug={news.Slug}
                                                    image={news && news.PostFiles.length > 0 ? news.PostFiles[0].AssetLiveUrl : "/assets/images/no-image.png"}
                                                    title={news.TitleData[0].Translation}
                                                    color={news.CategoryColor}
                                                    tag={news.CategoryName}
                                                    description={news.DescriptionData[0].Translation}
                                                />
                                                )
                                            })}

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </section>
        </Fragment>
    )

}