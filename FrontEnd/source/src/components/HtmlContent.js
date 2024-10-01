import DOMPurify from 'dompurify';
import React from 'react'

const HtmlContent = ({ html }) => {
    // Sanitize the HTML content
    const sanitizedHtml = DOMPurify.sanitize(html);
  
    return (
      <div dangerouslySetInnerHTML={{ __html: sanitizedHtml }} />
    );
  };

export default HtmlContent