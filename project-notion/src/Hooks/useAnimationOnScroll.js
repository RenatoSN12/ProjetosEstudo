function callback(entries, observer){
  entries.forEach(entry => {
    if(entry.isIntersecting){
      entry.target.classList.add('animate-slideFromRight')
    }
  });
}

const observer = new IntersectionObserver(callback, {threshold:[0.9]})

observer.observe(document.querySelector('.Advantage'))