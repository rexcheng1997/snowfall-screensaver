let GRAVITY = 0.03;
let snow = [];
let gravity;

function setup() {
  createCanvas(windowWidth, windowHeight);
  gravity = createVector(0, GRAVITY);
}

function draw() {
  background(0);
  if (snow.length < 750) {
    snow.push(new SnowFlake());
  }
  
  let force = createVector((mouseX - width / 2) / width, (mouseY - height / 2) / height);
  force.mult(GRAVITY);
  force.add(gravity);
  
  for (snowflake of snow) {
    snowflake.applyForce(force);
    snowflake.update();
    snowflake.render();
  }
}

function mousePressed() {
  for (snowflake of snow) {
    snowflake.spread(mouseX, mouseY);
  }
}
