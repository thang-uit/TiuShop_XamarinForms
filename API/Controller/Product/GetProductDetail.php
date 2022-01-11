<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$productID = isset($_POST["productID"]) && !empty($_POST["productID"]) ? $_POST["productID"] : "";

$arrayProduct = $product->getProductDetail($userID, $productID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Product"
);

echo json_encode($array);
