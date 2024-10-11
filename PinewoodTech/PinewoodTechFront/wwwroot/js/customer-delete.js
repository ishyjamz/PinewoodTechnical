document.addEventListener("DOMContentLoaded", function () {
  // Attach event listeners to all delete buttons
  const deleteButtons = document.querySelectorAll(".delete-btn");
  deleteButtons.forEach((button) => {
    button.addEventListener("click", function (event) {
      const customerId = event.target.getAttribute("data-id");
      if (confirm("Are you sure you want to delete this customer?")) {
        // Call your API to delete the customer
        deleteCustomer(customerId);
      }
    });
  });
});

function deleteCustomer(id) {
  fetch(`http://localhost:5134/api/Customer/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (response.ok) {
        // Remove the deleted customer from the UI (optional)
        document
          .querySelector(`button[data-id="${id}"]`)
          .closest("tr")
          .remove();
        alert("Customer deleted successfully");
      } else {
        alert("Failed to delete customer");
      }
    })
    .catch((error) => {
      console.error("Error deleting customer:", error);
      alert("Error deleting customer");
    });
}
