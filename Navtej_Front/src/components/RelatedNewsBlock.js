import React, { Fragment, props } from 'react'
import parse from "html-react-parser";
import configData from './Config';


export default function RelatedNewsBlock(props) {
    return (
        <div className="col-md-3 mycol">
            <div className="single-news landScape-normal box_design_Line">
                <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${props.slug}`}>
                    <div className="content-wrapper">
                        <div className="img">
                        <div className="tag" style={props && props.color ? { backgroundColor: props.color } : { backgroundColor: '#9c27b0' }}>
                                {props.tag}
                            </div>
                            <img src={props.image} alt={props.title} className="lazy" />
                        </div>
                        <div className="inner-content">
                            <h4 className="title">
                                {props.title}
                            </h4>
                            <p className="text">
                                {parse(props.description.substring(0, 75) + '...')}
                            </p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    )

}