%% Initialization
clear ; close all; clc

data   = load('wiiwands-test-data-oct.txt');
[m, n] = size(data);
y = data(:, n);
%X = data(:, 1:n-1);
X = data(:, 1:n-5); % Try without start/end positions
[m, n] = size(X);

input_layer_size  = n;  % height x width of Input Images + start X, start Y, end X and end Y
hidden_layer_size = 25; % Number of hidden units
num_labels = 16;        % Number spells

Theta1 = randInitializeWeights(input_layer_size, hidden_layer_size);
Theta2 = randInitializeWeights(hidden_layer_size, num_labels);

% Unroll parameters 
nn_params = [Theta1(:) ; Theta2(:)];

% Weight regularization parameter
%lambda = 1;

%J = nnCostFunction(nn_params, input_layer_size, hidden_layer_size, ...
%                   num_labels, X, y, lambda);

fprintf(['Cost at parameters: %f\n'], J);

%checkNNGradients;

%  Check gradients by running checkNNGradients
lambda = 3;
%checkNNGradients(lambda);

% Also output the costFunction debugging values
debug_J  = nnCostFunction(nn_params, input_layer_size, ...
                          hidden_layer_size, num_labels, X, y, lambda);

fprintf(['\n\nCost at (fixed) debugging parameters (w/ lambda = 10): %f ' ...
         '\n(this value should be about 0.576051)\n\n'], debug_J);

fprintf('\nTraining Neural Network... \n')

%  After you have completed the assignment, change the MaxIter to a larger
%  value to see how more training helps.
options = optimset('MaxIter', 100);

%  You should also try different values of lambda
lambda = 3;

% Create "short hand" for the cost function to be minimized
costFunction = @(p) nnCostFunction(p, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, X, y, lambda);

% Now, costFunction is a function that takes in only one argument (the
% neural network parameters)
[nn_params, cost] = fmincg(costFunction, nn_params, options);

% Obtain Theta1 and Theta2 back from nn_params
Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));
pred = predict(Theta1, Theta2, X);

fprintf('\nTraining Set Accuracy: %f\n', mean(double(pred == y)) * 100);

%dlmwrite('thetas.txt', [Theta1(:); Theta2(:)]);
dlmwrite('theta2.txt', Theta2);
dlmwrite('theta1.txt', Theta1);

%Theta1
%Theta2
%size(Theta1)
%size(Theta2)

% Test against test set

data   = load('wiiwands-test-data-testing-oct.txt');
[m, n] = size(data);
y = data(:, n);
%X = data(:, 1:n-1);
X = data(:, 1:n-5); % Try without start/end positions
pred = predict(Theta1, Theta2, X);
fprintf('\nTesting Set Accuracy: %f\n', mean(double(pred == y)) * 100);


% Test against validation set

data   = load('wiiwands-test-data-validation-oct.txt');
[m, n] = size(data);
y = data(:, n);
%X = data(:, 1:n-1);
X = data(:, 1:n-5); % Try without start/end positions
pred = predict(Theta1, Theta2, X);
fprintf('\nValidation Set Accuracy: %f\n', mean(double(pred == y)) * 100);


% Test against testing set

data   = load('wiiwands-test-data-training-oct.txt');
[m, n] = size(data);
y = data(:, n);
%X = data(:, 1:n-1);
X = data(:, 1:n-5); % Try without start/end positions
pred = predict(Theta1, Theta2, X);
fprintf('\nTraining Set Accuracy: %f\n', mean(double(pred == y)) * 100);

