import page from "page";

page("/", function () {
  document.getElementById("content").innerHTML =
    '<h1 class="display-4">Welcome to Majestic Motors</h1>';
});

page("/", function () {
  document.getElementById("content").innerHTML = "<h1>Customers</h1>";
});

page("/", function () {
  document.getElementById("content").innerHTML = "<h1>Add Customer</h1>";
});
