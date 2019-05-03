let SIZE_LOWER = 2;
let SIZE_UPPER = 12;
let SIZE_MIDDLE = 2 * (SIZE_LOWER + SIZE_UPPER) / (SIZE_UPPER - SIZE_LOWER);
let SMALL_MULTI = 0.4;
let LARGE_MULTI = 0.14;
let RANGE = 50;

function drawFlake(center, radius, alpha) {
  for (let i = 0; i < 6; i++) {
    // Draw the skeleton.
    stroke(255);
    strokeCap(ROUND);
    strokeWeight(radius / SIZE_UPPER * 4);
    let x = center.x + radius * cos(alpha + Math.PI / 3 * i);
    let y = center.y + radius * sin(alpha + Math.PI / 3 * i);
    line(center.x, center.y, x, y);
    /*
    // Draw the branches. 
     strokeWeight(radius / SIZE_UPPER);
     drawBranch(x / 3, y / 3, radius / 8, Math.PI / 3 + alpha + Math.PI / 3 * i);
     drawBranch(x / 3, y / 3, radius / 8, - Math.PI / 3 + alpha + Math.PI / 3 * i);
     drawBranch(2 * x / 3, 2 * y / 3, radius / 4, Math.PI / 3 + alpha + Math.PI / 3 * i);
     drawBranch(2 * x / 3, 2 * y / 3, radius / 4, - Math.PI / 3 + alpha + Math.PI / 3 * i);
     */
  }
}

function drawBranch(x, y, radius, angle) {
  let branchX = x + radius * cos(angle);
  let branchY = y + radius * sin(angle);
  line(x, y, branchX, branchY);
}

function getRandomSize(scale) {
  let r = randomGaussian() * scale;
  return constrain(abs(r * r), SIZE_LOWER, SIZE_UPPER);
}

class SnowFlake {

  constructor() {

    // Center of the flake.
    let x = random(width);
    let y = random(-100, -10);
    this.pos = createVector(x, y);

    // Falling speed of the flake.
    this.velocity = createVector(0, 0);
    this.accel = createVector();

    // Size of the flake.
    this.r = getRandomSize(2);
    this.angle = 0;
    this.dir = (random(1) < 0.5)? -1 : 1;

    // Shape of the flake.
    this.selector = (random(1) < 0.5);
  }

  regenerate() {
    let x = random(width);
    let y = random(-10, -1);
    this.pos = createVector(x, y);
    this.velocity = createVector(0, 0);
    this.accel = createVector();
    this.r = getRandomSize(2);
    this.angle = 0;
    this.dir = (random(1) < 0.5)? -1 : 1;
  }

  offScreen() {
    return (this.pos.y > height + this.r);
  }

  applyForce(force) {
    let f = force.copy();
    f.mult(this.r);
    this.accel.add(force);
  }

  update() {
    // Update velocity.
    this.velocity.add(this.accel);
    if (this.r < SIZE_MIDDLE) {
      this.velocity.limit(this.r * SMALL_MULTI);
    } else {
      this.velocity.limit(this.r * LARGE_MULTI);
    }

    // Update position.
    this.pos.add(this.velocity);
    this.angle += this.dir * Math.PI / 180 * this.r / SIZE_UPPER;
    this.accel.mult(0);
    this.pos.x += (1 - this.pos.y / width) / 2 * sin(this.angle);

    if (this.offScreen()) {
      this.regenerate();
    }
  }

  flash(filter) {
    if (random(1) <= filter) {
      if (this.selector) {
        drawFlake(this.pos, this.r, this.angle);
      } else {
        stroke(255);
        strokeWeight(this.r);
        point(this.pos.x, this.pos.y);
      }
    }
  }

  render() {
    if (this.r <= 3) {
      this.flash(0.9);
    } else {
      this.flash(1);
    }
  }

  spread(x, y) {
    if (abs(this.pos.x - x) < RANGE && abs(this.pos.y - y) < RANGE) {
      let force = createVector(this.pos.x - x, this.pos.y - y);
      this.applyForce(force);
    }
  }
}
