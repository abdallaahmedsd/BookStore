const maxValue = 40000;
const currentValue = 32455;
const percentage = (currentValue / maxValue) * 100;

// Get the progress circle
const circle = document.querySelector("#progress-circle");
const circleLength = 2 * Math.PI * 80; // Circumference for r=80

// Reverse progress (start from left)
circle.style.strokeDashoffset = circleLength * (1 - percentage / 100);

// Update text
document.querySelector("#progress-text").textContent = currentValue.toLocaleString();
