import React, { useEffect, useRef } from 'react';
import styles from './Advantage.module.css';

function Advantage({ title, text, image, inverse }) {
    const sectionRef = useRef(null);

    useEffect(() => {
        const callback = (entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const children = entry.target.children;
                    if (children.length === 2) {
                        children[0].classList.add(styles.slideLeft);
                        children[1].classList.add(styles.slideRight);
                    }
                    observer.unobserve(entry.target);
                }
            });
        };
    
        const observer = new IntersectionObserver(callback, { threshold: [0.5] });
    
        if (sectionRef.current) {
            observer.observe(sectionRef.current);
        }
    
        return () => {
            observer.disconnect();
        };
    }, []);

    return (
        <section ref={sectionRef} className={styles.Advantage}>
            {inverse ? (
                <>
                    <img 
                        src={image} 
                        alt="ilustrative-image" 
                        className={inverse ? styles.inverseImage : ''}
                    />
                    <div className={styles.Info}>
                        <h2>{title}</h2>
                        <p>{text}</p>
                    </div>
                </>
            ) : (
                <>
                    <div className={styles.Info}>
                        <h2>{title}</h2>
                        <p>{text}</p>
                    </div>
                    <img 
                        src={image} 
                        alt="ilustrative-image" 
                        className={inverse ? styles.inverseImage : ''}    
                    />
                </>
            )}
        </section>
    );
}

export default Advantage;
