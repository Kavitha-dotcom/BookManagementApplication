document.getElementById('getBooks').addEventListener('click', function () {
    fetch('/Books/Index')
        .then(response => response.text())
        .then(html => {
            document.getElementById('bookList').innerHTML = html;
        });
});
document.getElementById('hardcoverOnly').addEventListener('click', function () {
    fetch('/Books/HardcoverOnly')
        .then(response => response.text())
        .then(html => {
            document.getElementById('bookList').innerHTML = html;
        });
});