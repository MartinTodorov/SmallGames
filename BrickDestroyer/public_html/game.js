window.onload = function() {
    window.cancelRequestAnimFrame = (function() {
        return window.cancelAnimationFrame ||
                window.webkitCancelRequestAnimationFrame ||
                window.mozCancelRequestAnimationFrame ||
                window.oCancelRequestAnimationFrame ||
                window.msCancelRequestAnimationFrame ||
                clearTimeout
    })();
    window.requestAnimFrame = (function() {
        return  window.requestAnimationFrame ||
                window.webkitRequestAnimationFrame ||
                window.mozRequestAnimationFrame ||
                window.oRequestAnimationFrame ||
                window.msRequestAnimationFrame ||
                function(callback, element) {
                    return window.setTimeout(callback, 1000 / 60);
                };
    })();
    function animate() {
        draw();
        canvas.run = requestAnimFrame(animate);
    }// 
    var canvas = document.getElementById('canvas');
    if (canvas.getContext) {
        var context = canvas.getContext('2d');
        var ball = {};
        var pad = {};
        var brick = {};
        var hud = {};
        var ctrl = {};

        init();
        background();
        //creating the brrick
        brickInit();
        brickDraw();
        // set the paddle
        padInit();
        padDraw();
        //creating the ball
        ballInit();
        ballDraw();
    }
    function init() {
        brickInit();
        ballInit();
        padInit();
        animate();
    }
    function animate() {
        draw();
    }
    function draw() {

        context.clearRect(0, 0, canvas.width, canvas.height);
        background();
        brickDraw();
        padDraw();
        ballDraw();
    }
    function brickInit() {
        brick.gap = 2;
        brick.row = 3;
        brick.col = 5;
        brick.w = 80;
        brick.h = 15;

        brick.count = new Array(brick.row);
        for (i = 0; i < brick.row; i++) {
            brick.count[i] = new Array(brick.col);
        }
    }
    function brickDraw() {
        for (i = 0; i < brick.row; i++) {
            for (j = 0; j < brick.col; j++) {
                if (brick.count[i][j] !== false) {
                    context.fillStyle = 'blue';
                    context.fillRect(brickX(j), brickY(i), brick.w, brick.h);
                }
            }
        }
    }
    function brickX(row) {
        return (row * brick.w) + (row * brick.gap);
    }

    function brickY(col) {
        return (col * brick.h) + (col * brick.gap);
    }
    function brickColor(row) {
        y = brickY(row);
        var brickG = context.createLinearGradient(0, y, 0, y + brick.h);
        switch (row) {
            case 1:
                brickG.addColorStop(0, '#bd06f9');
                brickG.addColorStop(1, '#9604c7');
                break;
            case 2:
                brickG.addColorStop(0, '#F9064A');
                brickG.addColorStop(1, '#c7043b');
                break;
            case 3:
                brickG.addColorStop(0, '#05fa15');
                brickG.addColorStop(1, '#04c711');
                break;

            default:
                brickG.addColorStop(0, '#faa105');
                brickG.addColorStop(1, '#c77f04');
                break;
        }
        return context.fillStyle = brickG;
    }
    function padInit() {
        function padInit() {
            pad.x = 100;
            pad.y = 210;
            pad.w = 90;
            pad.h = 20;
            pad.r = 9;

        }
        function padDraw() {
            pad.x += pad.speed;
            context.beginPath();
            context.moveTo(pad.x, pad.y);
            context.arcTo(pad.x + pad.w, pad.y, pad.x + pad.w, pad.y + pad.r, pad.r);
            context.arcTo(pad.x + pad.w, pad.y + pad.h, pad.x + pad.w - pad.r, pad.y + pad.h,
                    pad.r);
            context.arcTo(pad.x, pad.y + pad.h, pad.x, pad.y + pad.h - pad.r, pad.r);
            context.arcTo(pad.x, pad.y, pad.x + pad.r, pad.y, pad.r);
            context.closePath();
            context.fillStyle = padGrad();
            context.fill();
        }
    }
    function padGrad() {
        var padG = context.createLinearGradient(pad.x, pad.y, pad.x, pad.y + 20);
        padG.addColorStop(0, '#eee');
        padG.addColorStop(1, '#999');
        return padG;
    }
    function ballInit() {
        ball.x = 120;
        ball.y = 120;
        ball.r = 10;
        ball.sx = 2;
        ball.sy = -2;
    }
    function ballDraw() {
        ball.x += ball.sx;
        ball.y += ball.sy;
        context.beginPath();
        context.arc(ball.x, ball.y, ball.r, 0, 2 * Math.PI, false);
        context.fillStyle = ballGrad();
        context.fill();
    }
    function ballGrad() {
        var ballG = context.createRadialGradient(ball.x, ball.y, 2, ball.x -
                4, ball.y - 3, 10);
        ballG.addColorStop(0, '#eee');
        ballG.addColorStop(1, '#999');
        return ballG;
    }
    function background() {
        var img = new Image();
        img.src = 'images(1).jpg';
        context.drawImage(img, 0, 0);
    }
}