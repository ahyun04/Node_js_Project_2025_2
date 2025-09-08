//HTTP 모듈 로딩

let http = reqiure("http");

//HTTP 서버를 Listen 상태로 8000포트를 사용하여 만든다
http.createServer(function (request, response){
    //response HTTP 타입 해더를 정의
    response.writeHead(200, {'Content-Type' : 'text/plain'});

    response.end("Hello world");
}).listen(800);

console.log("Server running at http://127.0.0.1:3000");