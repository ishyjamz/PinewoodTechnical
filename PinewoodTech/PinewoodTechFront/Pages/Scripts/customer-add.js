document
  .getElementById("addCustomerForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    const customer = {
      name: document.getElementById("name").value,
      email: document.getElementById("email").value,
    };

    fetch("/api/customers", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(customer),
    })
      .then((response) => response.json())
      .then((data) => {
        let customerList = document.getElementById("customerList");
        customerList.innerHTML += `<li>${data.name} - ${data.email}</li>`;
      })
      .catch((error) => console.error("Error:", error));
  });
