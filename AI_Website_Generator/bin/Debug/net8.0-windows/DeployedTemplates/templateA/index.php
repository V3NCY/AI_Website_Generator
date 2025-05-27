<?php
                                    /* Template: Template A */
                                    ?><!DOCTYPE html>
                                    <html>
                                    <head>
                                        <meta charset='UTF-8'>
                                        <title>Template A</title>
                                    </head>
                                    <body>
                                    <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Modern Business</title>
    <link rel="stylesheet" href="/templates/styleA.css">
</head>
<body>
    <header>
        <h1>Modern Business</h1>
        <nav>
            <a href="#">Home</a>
            <a href="#">Services</a>
            <a href="#">About</a>
            <a href="#">Contact</a>
        </nav>
    </header>
    <section class="hero">
        <h2>Professional Solutions for Your Business</h2>
        <p>We help you grow with innovative strategies.</p>
        <button>Get Started</button>
    </section>
    <section class="services">
        <h2>Our Services</h2>
        <div class="service-box">Web Development</div>
        <div class="service-box">Marketing</div>
        <div class="service-box">Consulting</div>
    </section>
</body>
</html>
<style>
    body {
        font-family: 'Poppins', Arial, sans-serif;
        margin: 0;
        padding: 0;
        background: #f4f7f6;
        color: #333;
        text-align: center;
    }

    /* Header */
    header {
        background: #2c3e50;
        padding: 20px;
        color: white;
        box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
    }

    nav a {
        color: white;
        text-decoration: none;
        margin: 0 15px;
        font-weight: bold;
        transition: color 0.3s ease-in-out;
    }

        nav a:hover {
            color: #f1c40f;
        }

    /* Hero Section */
    .hero {
        background: linear-gradient(135deg, #3498db, #6dd5fa);
        color: white;
        padding: 60px 20px;
        border-radius: 0 0 20px 20px;
        box-shadow: 0px 5px 10px rgba(0, 0, 0, 0.1);
    }

        .hero h2 {
            font-size: 2.2em;
            margin-bottom: 10px;
        }

        .hero p {
            font-size: 1.1em;
            margin-bottom: 20px;
        }

        .hero button {
            background: #f1c40f;
            border: none;
            padding: 12px 25px;
            font-size: 1.1em;
            font-weight: bold;
            color: #2c3e50;
            border-radius: 25px;
            cursor: pointer;
            transition: background 0.3s ease-in-out, transform 0.2s ease-in-out;
        }

            .hero button:hover {
                background: #f39c12;
                transform: scale(1.05);
            }

    /* Services Section */
    .services {
        margin: 40px auto;
        padding: 20px;
        max-width: 800px;
    }

        .services h2 {
            font-size: 2em;
            color: #2c3e50;
            margin-bottom: 20px;
        }

    .service-box {
        display: inline-block;
        background: #ffffff;
        padding: 20px;
        margin: 15px;
        width: 220px;
        border-radius: 15px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        font-weight: bold;
        color: #2c3e50;
        transition: transform 0.2s ease-in-out, box-shadow 0.3s ease-in-out;
    }

        .service-box:hover {
            transform: translateY(-5px);
            box-shadow: 0px 6px 12px rgba(0, 0, 0, 0.15);
        }

    /* Responsive Design */
    @media (max-width: 768px) {
        .hero {
            padding: 40px 15px;
        }

        .services {
            padding: 10px;
        }

        .service-box {
            width: 90%;
            margin: 10px auto;
        }
    }

</style>
                                    </body>
                                    </html>